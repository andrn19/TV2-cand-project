import React, { useState } from 'react';
import { FaRegEdit } from "react-icons/fa";
import { VideoMetadateClass } from '../../classes/videoMetadataClass';

interface ReceivedVideoPreviewProps {
  metadateObject: VideoMetadateClass;
  onEdit: () => void;
}

const ReceivedVideoPreview: React.FC<ReceivedVideoPreviewProps> = ({ metadateObject, onEdit }) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <>
      <li data-testid='video-preview'
        className='relative rounded-md mb-3 ml-3'
        onMouseEnter={() => setIsHovered(true)}
        onMouseLeave={() => setIsHovered(false)}
      >
        <video
          controls
          className='object-contain rounded-md'
        >
          <source src={metadateObject.publishedUrl || ''} />
          Your browser does not support the video tag.
        </video>

        {isHovered && (
          <>
            <button
              data-testid='edit-btn'
              className='preview-edit-btn'
              onClick={onEdit}
            >
              <FaRegEdit className='icon-style' />
            </button>
          </>
        )}
        <p className='mt-2 text-neutral-500 text-[12px] font-medium'>
          {metadateObject.videoName}
        </p>
      </li>
    </>
  );
};

export default ReceivedVideoPreview;