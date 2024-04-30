import React from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass';

type MetadataVideoPreviewProps = {
    metadateObject: VideoMetadateClass | undefined;
}

const MetadataVideoPreview: React.FC<MetadataVideoPreviewProps> = ({ metadateObject }) => {
    return (
        <div>
            <video
                controls
                className='object-contain rounded-md w-1/2 h-auto mx-auto'
            >
                <source src={metadateObject?.publishedUrl || ''} />
                Your browser does not support the video tag.
            </video>
        </div>
    )
}

export default MetadataVideoPreview