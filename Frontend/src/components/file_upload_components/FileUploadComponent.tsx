import { useState } from 'react'
import { signal } from '@preact/signals-react';
import { useSignals } from '@preact/signals-react/runtime';
import { FileWithPath } from 'react-dropzone';
import { VideoMetadateClass } from '../../classes/videoMetadataClass';

//Components
import FileDropzone from './FileDropzone';
import FileList from './FileList';
import AnalyseVideoButton from './AnalyseVideoButton';


const receivedVideoData = signal<VideoMetadateClass[]>([])

const FileUploadComponent = () => {

    useSignals()

    const [uploadedFiles, setUploadedFiles] = useState<FileWithPath[]>([]);

    const addNewReceivedVideoData = (newData: VideoMetadateClass) => {
        if (receivedVideoData.value === undefined) {
            return
        }
        const dataExists = receivedVideoData.value.some(data => data.videoId === newData.videoId);

        if (!dataExists) {
            receivedVideoData.value = [...receivedVideoData.value, newData];
        }
        else {
            alert('Scame with that name is already created')
        }
    };

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