import { FileWithPath } from 'react-dropzone';
import FilePreview from './FilePreview';

interface FileListProps {
    files: FileWithPath[];
    updateFiles: (acceptedFiles: FileWithPath[]) => void;
}

const FileList: React.FC<FileListProps> = ({ files, updateFiles }) => {


    const handleRemoveFile = (fileToRemove: FileWithPath) => {
        const updatedFiles = files.filter(file => file !== fileToRemove);
        console.log(updatedFiles)
        updateFiles(updatedFiles)
      };

    return (
        <div className="rounded-lg bg-gray-700 flex flex-col">
            <h2 className="font-bold">Uploaded Files:</h2>
            <ul className='mt-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6 gap-10'>
                {files.map(file => (
                    <div key={file.name} >
                        <FilePreview file={file} onRemove={() => handleRemoveFile(file)}/>
                    </div>
                ))}
            </ul>
        </div>
    );
};

export default FileList;