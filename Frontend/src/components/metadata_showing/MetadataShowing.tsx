import { useEffect, useState } from 'react'
import { Emotion, Topic, Label, Keyword, NamedLocation, NamedPeople, Transcript, Face, Shot, VideoMetadateClass, Metadata } from '../../classes/videoMetadataClass';

import MetadataVideoPreview from './MetadataVideoPreview';
import SaveMetadataButton from './SaveMetadataButton';
import KeyframeListing from './KeyframeListing';

type MetadataShowingProps = {
    metadateObject: VideoMetadateClass;
};

enum MetadataKey {
    emotions = 'type',
    faces = 'name',
    keywords = 'text',
    labels = 'name',
    namedLocations = 'name',
    namedPeople = 'name',
    topics = 'name',
    transcript = 'text',
}

const MetadataShowing: React.FC<MetadataShowingProps> = ({ metadateObject }) => {
    const [dataState, setDataState] = useState<VideoMetadateClass>(metadateObject);

    useEffect(() => {
        const textareas = document.querySelectorAll("textarea#transcript-area");
        textareas.forEach((value: Element) => {
            const textarea = value as HTMLTextAreaElement;
            textarea.style.height = 'auto';
            textarea.style.height = textarea.scrollHeight + 'px';
        });
    }, []);

    const getInstancesForMetadata = (metadata: Emotion | Topic | Label | Keyword | NamedLocation | NamedPeople | Transcript | Face | Shot): string => {
        return metadata['instances'].map((instance) => {
            return `(${instance.adjustedStart} - ${instance.adjustedEnd})`
        }).join(', ')
    }

    const getMetadataValueByKey = (key: string, metadata: Emotion | Topic | Label | Keyword | NamedLocation | NamedPeople | Transcript | Face | Shot): string => {
        switch (key) {
            case 'emotions':
                return `${(metadata as Emotion)[MetadataKey[key]]}`;
            case 'faces':
                return `${(metadata as Face)[MetadataKey[key]]}`;
            case 'keywords':
                return `${(metadata as Keyword)[MetadataKey[key]]}`;
            case 'labels':
                return `${(metadata as Label)[MetadataKey[key]]}`;
            case 'namedLocations':
                return `${(metadata as NamedLocation)[MetadataKey[key]]}`;
            case 'namedPeople':
                return `${(metadata as NamedPeople)[MetadataKey[key]]}`;
            case 'topics':
                return `${(metadata as Topic)[MetadataKey[key]]}`;
            case 'transcript':
                return `${(metadata as Transcript)[MetadataKey[key]]}`;
            default:
                return '';
        }
    };

    const handleContentEdit = (key: string, index: number, content: string) => {
        const newDataState = { ...dataState };
        const metadataKey = MetadataKey[key as keyof typeof MetadataKey];
        (newDataState[key as keyof Metadata]?.[index] as any)[metadataKey] = content;
        setDataState(newDataState);
    };

    const handleTextAreaHeight = (e: React.FormEvent<HTMLTextAreaElement>) => {
        e.currentTarget.style.height = 'auto';
        e.currentTarget.style.height = e.currentTarget.scrollHeight + 'px';
    }

    return (
        <div>
            <h1 className='pb-3'>Editing the metadata for <br /> "{metadateObject?.videoName}"</h1>
            <div className='overflow-y-auto' style={{ maxHeight: '85vh' }}>
                <MetadataVideoPreview metadateObject={metadateObject} />
                <div className='mt-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-3'>
                    {Object.keys(dataState).map((key) => (
                        !(key === 'shots') ?
                            <div data-testid='metadata-div' key={key} className="light p-2 rounded shadow-md">
                                <h2>{key}</h2>
                                {Array.isArray(dataState[key as keyof Metadata]) && dataState[key as keyof Metadata]?.map((entry, index) => (
                                    <div key={entry.id}>
                                        <textarea
                                            data-testid={key === 'transcript' ? 'transcript-textarea' : 'metadata-textarea'}
                                            id={key === 'transcript' ? 'transcript-area' : undefined}
                                            value={getMetadataValueByKey(key, entry)}
                                            onChange={(e) => handleContentEdit(key, index, e.target.value)}
                                            onInput={(e) => handleTextAreaHeight(e)}
                                            rows={1}
                                            style={{ color: 'black', width: '100%' }}
                                            className='light pl-1 pr-1 block overflow-hidden resize-none'
                                        />
                                        <p className='pb-3 border-b border-gray-300'>Time instances: {getInstancesForMetadata(entry)}</p>
                                    </div>
                                ))}
                            </div>
                            :
                            null
                    ))}
                </div>
                {dataState['shots'] &&
                    <div className='light mt-3 p-2 rounded shadow-md'>
                        <h2>Keyframes</h2>
                        <KeyframeListing key='shots' keyframes={dataState['shots']} />
                    </div>
                }
                <SaveMetadataButton metadata={dataState} />
            </div>
        </div>
    );
}

export default MetadataShowing