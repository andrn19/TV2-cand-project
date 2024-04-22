import { effect, signal } from '@preact/signals-react';
import { useSignals } from '@preact/signals-react/runtime';
import { SchemaInfo } from './interfaces'

//components
import FileUploadComponent from './components/file_upload_components/FileUploadComponent';
import SchemaCreator from './components/schema_components/SchemaCreator';
import SchemaSeletor from './components/schema_components/SchemaSeletor';
import DatabaseConnectorTab from './components/database_connector_components/DatabaseConnectorTab';

import './App.css'

const LOCAL_STORAGE_KEY_SCHEMAS = "SCHEMAS"


const getLocalSchemas = () => {
  const value = localStorage.getItem(LOCAL_STORAGE_KEY_SCHEMAS)
  if(value == null) return []
  return JSON.parse(value)
}
const schemas = signal<SchemaInfo[]>(getLocalSchemas())
effect(() => {
  localStorage.setItem(LOCAL_STORAGE_KEY_SCHEMAS, JSON.stringify(schemas.value))
})


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


  return (
    <div>
      <DatabaseConnectorTab />
      <h1 className='text-2xl font-bold mt-4 mb-7'>File Drop Zone</h1>
      <FileUploadComponent />
      <SchemaSeletor schemas={schemas.value} />
      <SchemaCreator addNewSchema={addNewSchema} />
    </div>
  );
}

export default App
