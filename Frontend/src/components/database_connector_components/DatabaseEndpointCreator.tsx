import { useState } from 'react'
import { DataInterface, EndpointFormData } from '../../interfaces';

interface EndpointCreatorProps {
    fetchEndpointList: () => void;
    isDupInConnectors: (newConnector: EndpointFormData) => boolean;
}

const CREATE_ENDPOINT_API_URL = 'http://localhost:52001/gateway/create-endpoint';

const DatabaseEndpointCreator: React.FC<EndpointCreatorProps> = ({ fetchEndpointList, isDupInConnectors }) => {
    const [formData, setFormData] = useState<EndpointFormData>({
        name: 'Name',
        url: 'Endpoint URL',
        port: 0,
    });


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (isDupInConnectors(formData)) {
            return
        }

        console.log(JSON.stringify(formData))

        const requestOptions = {
            method: 'PUT',
            headers: {
                Accept: 'text/plain',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),
        };

        try {
            const response = await fetch(CREATE_ENDPOINT_API_URL, requestOptions);
            if (response.ok) {
                fetchEndpointList()
            }
            else {
                console.error('Failed with status', response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <div>
            <label className="text-white block font-bold mb-2">Add database:</label>
            <form onSubmit={handleSubmit} id="endpointCreator">
                {Object.keys(formData).map((key) =>
                    <div key={key}>
                        <label htmlFor={key}>{key.toUpperCase()}:</label>
                        <input
                            type={key === 'url' ? "url" : key === 'port' ? "number" : "text"}
                            id={key}
                            name={key}
                            className="appearance-none bg-gray-700 rounded-md p-.5 text-white w-full"
                            value={formData[key as keyof typeof formData]}
                            onChange={(e) => setFormData({ ...formData, [key]: e.target.value })}
                        />
                    </div>
                )}
                <button type="submit" >
                    Create
                </button>
            </form>
        </div>
    );
};

export default DatabaseEndpointCreator;