import { useState } from 'react'
import { FileWithPath } from 'react-dropzone';

//components
import FileDropzone from './components/FileDropzone'
import FileList from './components/FileList';
import FeatureChecklist from './components/FeatureCheckList';


import './App.css'

function App() {
  const [uploadedFiles, setUploadedFiles] = useState<FileWithPath[]>([]);

  const handleFilesDrop = (acceptedFiles: FileWithPath[]) => {

    const filesCopy = [...uploadedFiles]
    console.log(filesCopy)
    const newFiles = acceptedFiles.filter(file => {
      return !filesCopy.find(prevFile => prevFile.name === file.name);
    });

    console.log(newFiles)

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
      <h1 className='text-2xl font-bold mt-4 mb-7'>React Drag-and-Drop Zone</h1>
      <FileDropzone onFilesDrop={handleFilesDrop} />
      <FileList files={uploadedFiles} updateFiles={updateFiles} />
      <FeatureChecklist />
    </div>
  );
}

export default App
