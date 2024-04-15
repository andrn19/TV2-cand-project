import { signal } from '@preact/signals-react';
import { useSignals } from '@preact/signals-react/runtime';
import { SchemaInfo } from './interfaces'

//components
import FileUploadComponent from './components/file_upload_components/FileUploadComponent';
import SchemaCreator from './components/schema_components/SchemaCreator';
import SchemaSeletor from './components/schema_components/SchemaSeletor';
import AnalyseVideoButton from './components/file_upload_components/AnalyseVideoButton';
import DatabaseConnectorTab from './components/database_connector_components/DatabaseConnectorTab';

import './App.css'

const schemas = signal<SchemaInfo[]>([])


function App() {

  useSignals();


  const addNewSchema = (newSchema: SchemaInfo) => {
    schemas.value = [...schemas.value, newSchema];
  };


  return (
    <div>
      <DatabaseConnectorTab />
      <h1 className='text-2xl font-bold mt-4 mb-7'>File Drop Zone</h1>
      <FileUploadComponent />
      <SchemaSeletor schemas={schemas.value} />
      <AnalyseVideoButton />
      <SchemaCreator addNewSchema={addNewSchema} />
    </div>
  );
}

export default App
