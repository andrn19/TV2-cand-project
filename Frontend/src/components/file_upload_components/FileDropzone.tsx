import React, { useCallback } from 'react';
import { useDropzone, FileWithPath } from 'react-dropzone';

interface DropzoneProps {
  onFilesDrop: (acceptedFiles: FileWithPath[]) => void;
}

const acceptedVideoTypes = ['video/mp4', 'video/avi', 'video/mov'];

const FileDropzone: React.FC<DropzoneProps> = ({ onFilesDrop }) => {



  const onDrop = useCallback((acceptedFiles: FileWithPath[]) => {

    const videoFiles = acceptedFiles.filter(file => acceptedVideoTypes.includes(file.type));

    onFilesDrop(videoFiles);

  }, [onFilesDrop]);

  const { getRootProps, getInputProps } = useDropzone({ onDrop });

  return (
    <div {...getRootProps()} className='border-dashed border-2 border-gray-400 p-4 mb-10 transition duration-300 hover:bg-gray-800 cursor-pointer' >
      <input {...getInputProps()} />
      <p className='text-gray-600'>Drag & drop files here, or click to select files</p>
    </div>
  );
};

export default FileDropzone;