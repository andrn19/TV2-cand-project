import { useState, useEffect } from 'react'
import { Schemas, SchemaInfo } from '../../interfaces';

const SCHEMA_LIST_API_URL = '';



const SchemaSeletor: React.FC<Schemas> = ({ schemas }) => {
    const [selectedSchema, setSelectedConnector] = useState<SchemaInfo>();

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
        const valueObject = JSON.parse(event.target.value)
        setSelectedConnector(valueObject);
    };



    return (
        <>
            <div className="my-4">
                <label htmlFor="schemaSelector" className="text-white block font-bold mb-2">Select a schema:</label>
                <select className="appearance-none bg-gray-700 rounded-md p-.5 text-white" id="schemaSelector" onChange={handleSelectChange}>
                    {/* Placeholder option */}
                    <option value="" hidden>
                        Choose a schema
                    </option>
                    {schemas.map((schema) => (
                        <option key={schema.name} value={JSON.stringify(schema)}>{schema.name}</option>
                    ))}
                </select>
            </div>
            <div className="rounded-lg bg-gray-700 flex flex-col mb-5">
                <h4 className="text-lg font-bold">Metadata to be generated:</h4>
                {selectedSchema?.insights.map((insight) => (
                    <p>{insight}</p>
                ))}
            </div>
        </>
    );
}

export default SchemaSeletor