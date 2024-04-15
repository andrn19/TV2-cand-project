import { useState, useEffect } from 'react'
import { DataInterface } from '../../interfaces';

interface DatabaseConnectorProps {
    databaseConnectors: DataInterface[]
}

const DatabaseConnector: React.FC<DatabaseConnectorProps> = ({databaseConnectors}) => {

    const [selectedConnector, setSelectedConnector] = useState('');

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedConnector(event.target.value);
    };


    return (
        <div className="my-4">
            <label htmlFor="databaseSelector" className="text-white block font-bold mb-2">Select a database:</label>
            <select className="appearance-none bg-gray-700 rounded-md p-.5 text-white" id="databaseSelector" onChange={handleSelectChange}>
                {/* Placeholder option */}
                <option value="" hidden>
                    Choose a database
                </option>
                {databaseConnectors.map((connector) => (
                    <option key={connector.key} value={connector.value}>{connector.value}</option>
                ))}
            </select>
        </div>
    );
}

export default DatabaseConnector