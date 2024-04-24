import { useState } from 'react'
import { DataInterface } from '../../interfaces';

interface DatabaseConnectorProps {
    databaseConnectors: DataInterface[];
    removeConnector: (connectorId: string) => void;
}

const DatabaseConnector: React.FC<DatabaseConnectorProps> = ({ databaseConnectors, removeConnector }) => {

    const [selectedConnector, setSelectedConnector] = useState<DataInterface>();

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const valueObject = JSON.parse(event.target.value)
        setSelectedConnector(valueObject);
    };

    const handleRemoveClick = () => {
        if (selectedConnector) {
            removeConnector(selectedConnector.key);
            setSelectedConnector(undefined);
        }
    };

    const handleConnectClick = () => {
        if (selectedConnector) {

        }
    };

    return (
        <div className="my-4">
            <label htmlFor="databaseSelector" className="text-white block font-bold mb-2">Select a database:</label>
            <select data-testid="database-selector" className="appearance-none bg-gray-700 rounded-md p-.5 text-white" id="databaseSelector" onChange={handleSelectChange}>
                {/* Placeholder option */}
                <option value="" hidden>
                    Choose a database
                </option>
                {databaseConnectors.map((connector) => (
                    <option data-testid="connector-option" key={connector.key} value={JSON.stringify(connector)}>{connector.value}</option>
                ))}
            </select>
            <div className="mt-4">
                <button data-testid="database-connect-btn" onClick={handleConnectClick} >
                    Connect
                </button>
                <button data-testid="database-remove-btn" onClick={handleRemoveClick} className='bg-red-500 hover:bg-red-600' >
                    Remove
                </button>
            </div>
        </div>
    );
}

export default DatabaseConnector;