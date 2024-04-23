import { effect, signal } from '@preact/signals-react';
import { useSignals } from '@preact/signals-react/runtime';
import { SchemaInfo } from './interfaces'
import { VideoMetadateClass } from './classes/videoMetadataClass';

//components
import FileUploadComponent from './components/file_upload_components/FileUploadComponent';
import SchemaCreator from './components/schema_components/SchemaCreator';
import SchemaSeletor from './components/schema_components/SchemaSeletor';
import DatabaseConnectorTab from './components/database_connector_components/DatabaseConnectorTab';
import ReceivedMetadataListing from './components/metadata_showing/ReceivedMetadataListing';

import './App.css'

const LOCAL_STORAGE_KEY_SCHEMAS = "SCHEMAS"


const getLocalSchemas = () => {
  const value = localStorage.getItem(LOCAL_STORAGE_KEY_SCHEMAS)
  if (value == null) return []
  return JSON.parse(value)
};

const schemas = signal<SchemaInfo[]>(getLocalSchemas())
effect(() => {
  localStorage.setItem(LOCAL_STORAGE_KEY_SCHEMAS, JSON.stringify(schemas.value))
});

const receivedVideoData = signal<VideoMetadateClass[]>([]);

function App() {

  useSignals();

  const addNewSchema = (newSchema: SchemaInfo) => {
    const schemaExists = schemas.value.some(schema => schema.name === newSchema.name);

    if (!schemaExists) {
      schemas.value = [...schemas.value, newSchema];
    }
    else {
      alert('Scame with that name is already created')
    }
  };

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

  return (
    <div>
      <DatabaseConnectorTab />
      <h1 className='text-2xl font-bold mt-4 mb-7'>File Drop Zone</h1>
      <FileUploadComponent addNewReceivedVideoData={addNewReceivedVideoData} />
      <SchemaSeletor schemas={schemas.value} />
      <ReceivedMetadataListing receivedVideoData={receivedVideoData.value} />
      <SchemaCreator addNewSchema={addNewSchema} />
    </div>
  );
}

export default App
