import { useState } from 'react'
import { FileWithPath } from 'react-dropzone';
import { VideoMetadateClass } from '../../classes/videoMetadataClass';

//Components
import FileDropzone from './FileDropzone';
import FileList from './FileList';
import AnalyseVideoButton from './AnalyseVideoButton';

type FileUploadComponentProps = {
    addNewReceivedVideoData: (newData: VideoMetadateClass) => void;
}


const FileUploadComponent: React.FC<FileUploadComponentProps> = ({ addNewReceivedVideoData }) => {
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
            <AnalyseVideoButton addNewReceivedVideoData={addNewReceivedVideoData} />
        </div>
    )
}

export default FileUploadComponent