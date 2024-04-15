import { useEffect, useState } from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass';
import { FileWithPath } from 'react-dropzone';
import { Emotion, Topic, Label, Keyword, NamedLocation, NamedPeople, Transcript, Face } from '../../classes/videoMetadataClass';

interface CustomFileWithPath extends FileWithPath {
    preview?: string;
}

type MetadataShowingProps = {
    file: CustomFileWithPath | undefined;
    data: VideoMetadateClass;
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

const MetadataShowing: React.FC<MetadataShowingProps> = ({ file, data }) => {
    const [dataState, setDataState] = useState<VideoMetadateClass>(data);

    useEffect(() => {
        const textareas = document.querySelectorAll("textarea#transcript-area");
        textareas.forEach((value: Element) => {
            const textarea = value as HTMLTextAreaElement;
            textarea.style.height = 'auto';
            textarea.style.height = textarea.scrollHeight + 'px';
        });
    }, []);

    const getInstancesForMetadata = (metadata: Emotion | Topic | Label | Keyword | NamedLocation | NamedPeople | Transcript | Face): string => {
        return metadata['instances'].map((instance) => {
            return `(${instance.adjustedStart} - ${instance.adjustedEnd})`
        }).join(', ')
    }

    const getMetadataValueByKey = (key: string, metadata: Emotion | Topic | Label | Keyword | NamedLocation | NamedPeople | Transcript | Face): string => {
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
        (newDataState[key as keyof typeof dataState][index] as any)[metadataKey] = content;
        setDataState(newDataState);
    };

    const handleTextAreaHeight = (e: React.FormEvent<HTMLTextAreaElement>) => {
        e.currentTarget.style.height = 'auto';
        e.currentTarget.style.height = e.currentTarget.scrollHeight + 'px';
    }

    return (
        <div>
            <h1 className='pb-3'>Editing the metadata for <br /> "{file?.name}"</h1>
            <div className='overflow-y-auto' style={{ maxHeight: '85vh' }}>
                <div>
                    <video
                        controls
                        className='object-contain rounded-md w-1/2 h-auto mx-auto'
                        onLoad={() => {
                            if (file?.preview) {
                                URL.revokeObjectURL(file?.preview);
                            }
                        }}
                    >
                        <source src={file?.preview || ''} type={file?.type} />
                        Your browser does not support the video tag.
                    </video>
                </div>
                <div className='mt-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-3'>
                    {Object.keys(dataState).map((key) => (
                        <div key={key} className="light p-2 rounded shadow-md">
                            <h2>{key}</h2>
                            {dataState[key as keyof typeof dataState].map((entry, index) => (
                                <div key={entry.id}>
                                    <textarea
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
                    ))}
                </div>
            </div>
        </div>
    );
}

export default MetadataShowing