import { useEffect, useRef } from 'react'

import MetadataShowing from './MetadataShowing'

import { VideoMetadateClass } from '../../classes/videoMetadataClass'


interface MetadataWindowProps {
    metadateObject: VideoMetadateClass | undefined
    onClose: () => void;
}

const MetadataWindow: React.FC<MetadataWindowProps> = ({ metadateObject, onClose }) => {

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

    return (
        <div className="fixed inset-0 flex items-center justify-center z-50">
            <div ref={windowRef} className="relative dark p-2 rounded shadow-md" style={{ width: '95%' }}>
                <button
                    className='absolute -top-2 -right-2'
                    onClick={onClose}
                >
                    X
                </button>
                {
                    !metadateObject ?
                        <></>
                        :
                        <MetadataShowing metadateObject={metadateObject} />
                }
            </div>
        </div>
    )
}

export default MetadataWindow