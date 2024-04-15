import { useState } from 'react';
import { FileWithPath } from 'react-dropzone';

//Components
import MetadataWindow from '../metadata_showing/MetadataWindow';
import FilePreview from './FilePreview';

interface ShowMetadataInterface {
    show: boolean;
    file: FileWithPath | undefined;
}

interface FileListProps {
    files: FileWithPath[];
    updateFiles: (acceptedFiles: FileWithPath[]) => void;
}

const FileList: React.FC<FileListProps> = ({ files, updateFiles }) => {

    const [showMetadata, setShowMetadata] = useState<ShowMetadataInterface>({ show: false, file: undefined });

    const onEdit = (file: FileWithPath) => {
        setShowMetadata({ show: true, file: file });
    }

    const handleRemoveFile = (fileToRemove: FileWithPath) => {
        const updatedFiles = files.filter(file => file !== fileToRemove);
        console.log(updatedFiles)
        updateFiles(updatedFiles)
    };

    const closeEditWindow = () => {
        console.log('clicekd')
        setShowMetadata(prevState => ({
            ...prevState,
            show: false
        }))
    }

    return (
        <>
            <div className="rounded-lg bg-gray-700 flex flex-col">
                <h2 className="font-bold">Uploaded Files:</h2>
                <ul className='mt-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-10'>
                    {files.map(file => (
                        <div key={file.name} >
                            <FilePreview file={file} onRemove={() => handleRemoveFile(file)} onEdit={() => onEdit(file)} />
                        </div>
                    ))}
                </ul>
            </div>
            <div>
                {showMetadata.show && <MetadataWindow file={showMetadata.file} onClose={closeEditWindow} />}
            </div>
        </>
    );
};

export default FileList;