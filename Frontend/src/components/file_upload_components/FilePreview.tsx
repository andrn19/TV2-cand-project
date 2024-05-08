import React, { useState } from 'react';
import { FileWithPath } from 'react-dropzone';

interface CustomFileWithPath extends FileWithPath {
  preview?: string;
}

interface FilePreviewProps {
  file: CustomFileWithPath;
  onRemove: () => void;
}

const FilePreview: React.FC<FilePreviewProps> = ({ file, onRemove }) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <>
      <li className='relative rounded-md  mb-3 ml-3'
        onMouseEnter={() => setIsHovered(true)}
        onMouseLeave={() => setIsHovered(false)}
      >
        <video
          controls
          className='object-contain rounded-md'
          onLoad={() => {
            if (file.preview) {
              URL.revokeObjectURL(file.preview);
            }
          }}
        >
          <source src={file.preview || ''} type={file.type} />
          Your browser does not support the video tag.
        </video>

        {isHovered && (
          <>
            <button
              className='preview-delete-btn'
              onClick={onRemove}
            >
              X
            </button>
          </>
        )}
        <p className='mt-2 text-white-500 text-[12px] font-medium'>
          {file.name}
        </p>
      </li>
    </>
  );
};

export default FilePreview;
