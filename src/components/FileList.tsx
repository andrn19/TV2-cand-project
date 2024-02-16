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
        <div className="rounded-lg bg-gray-700">
            <h2 className="font-bold">Uploaded Files:</h2>
            <div className="flex flex-wrap gap-4">
                {files.map(file => (
                    <div key={file.name}>
                        <FilePreview file={file} onRemove={() => handleRemoveFile(file)}/>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default FileList;