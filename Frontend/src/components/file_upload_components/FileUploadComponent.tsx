import { useState } from 'react'
import { FileWithPath } from 'react-dropzone';

//Components
import FileDropzone from './FileDropzone';
import FileList from './FileList';

const FileUploadComponent = () => {

    const [uploadedFiles, setUploadedFiles] = useState<FileWithPath[]>([]);

    const handleFilesDrop = (acceptedFiles: FileWithPath[]) => {

        const filesCopy = [...uploadedFiles]

        const newFiles = acceptedFiles.filter(file => {
            return !filesCopy.find(prevFile => prevFile.name === file.name);
        });

        setUploadedFiles(previousFiles => [
            ...previousFiles,
            ...newFiles.map(file =>
                Object.assign(file, { preview: URL.createObjectURL(file) })
            )
        ]);
    };

    const updateFiles = (updatedFileList: FileWithPath[]) => {
        setUploadedFiles(updatedFileList)
    }

    return (
        <div>
            <FileDropzone onFilesDrop={handleFilesDrop} />
            <FileList files={uploadedFiles} updateFiles={updateFiles} />
        </div>
    )
}

export default FileUploadComponent