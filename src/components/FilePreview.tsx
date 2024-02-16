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
    <div
      className='relative rounded-lg border border-gray-300 shadow-md overflow-hidden m-3'
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <video
        controls
        width={200}
        className='object-cover'
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
        <button
          className='absolute top-2 right-2 bg-red-500 text-white p-1 rounded-full cursor-pointer'
          onClick={onRemove}
        >
          X
        </button>
      )}
    </div>
  );
};

export default FilePreview;
