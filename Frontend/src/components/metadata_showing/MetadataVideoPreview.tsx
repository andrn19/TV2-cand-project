import React from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass';

type MetadataVideoPreviewProps = {
    file: VideoMetadateClass | undefined;
}

const MetadataVideoPreview: React.FC<MetadataVideoPreviewProps> = ({ file }) => {
    return (
        <div>
            <video
                controls
                className='object-contain rounded-md w-1/2 h-auto mx-auto'
            >
                <source src={file?.publishedUrl || ''} />
                Your browser does not support the video tag.
            </video>
        </div>
    )
}

export default MetadataVideoPreview