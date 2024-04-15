import { useEffect, useState, useRef } from 'react'
import { FileWithPath } from 'react-dropzone';

import MetadataShowing from './MetadataShowing'

import data from '../../classes/data.json'
import { VideoMetadateClass } from '../../classes/videoMetadataClass'


interface MetadataWindowProps {
    file: FileWithPath | undefined
    onClose: () => void;
}

const MetadataWindow: React.FC<MetadataWindowProps> = ({ file, onClose }) => {
    const [metadataToShow, setMetadataToShow] = useState<VideoMetadateClass>()

    const windowRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (windowRef.current && !windowRef.current.contains(event.target as Node)) {
                onClose();
            }
        };

        document.addEventListener('mousedown', handleClickOutside);

        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, [onClose]);

    useEffect(() => {
        const metadata = new VideoMetadateClass(data)
        setMetadataToShow(metadata)
    }, [])

    return (
        <div className="fixed inset-0 flex items-center justify-center z-50">
            <div ref={windowRef} className="relative dark p-2 rounded shadow-md" style={{ width: '90%' }}>
                <button
                    className='absolute -top-2 -right-2'
                    onClick={onClose}
                >
                    X
                </button>
                {
                    !metadataToShow ?
                        <></>
                        :
                        <MetadataShowing file={file} data={metadataToShow} />
                }
            </div>
        </div>
    )
}

export default MetadataWindow