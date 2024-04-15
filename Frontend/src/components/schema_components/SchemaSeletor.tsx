import { useState, useEffect } from 'react'
import { Schemas } from '../../interfaces';

const SCHEMA_LIST_API_URL = '';



const SchemaSeletor: React.FC<Schemas> = ({ schemas }) => {
    const [selectedSchema, setSelectedConnector] = useState('');

    // useEffect(() => {
    //     fetch(SCHEMA_LIST_API_URL, {
    //         method: 'GET',
    //         headers: {
    //             'accept': 'text/plain'
    //         }
    //     })
    //         .then(response => response.json())
    //         .then(data => schemas.value(data))
    //         .catch(error => console.error(error));
    // }, [])

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedConnector(event.target.value);
    };


    
    return (
        <div className="my-4">
            <label htmlFor="schemaSelector" className="text-white block font-bold mb-2">Select a schema:</label>
            <select className="appearance-none bg-gray-700 rounded-md p-.5 text-white" id="schemaSelector" onChange={handleSelectChange}>
                {/* Placeholder option */}
                <option value="" hidden>
                    Choose a schema
                </option>
                {JSON.stringify(schemas)}
                {schemas.map((schema) => (
                    <option key={schema.name} value={schema.name}>{schema.name}</option>
                ))}
            </select>
        </div>
    );
}

export default SchemaSeletor