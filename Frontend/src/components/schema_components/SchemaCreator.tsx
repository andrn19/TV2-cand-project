import { useState } from 'react'
import FeatureChecklist from './FeatureCheckList'
import { SchemaInfo } from '../../interfaces'


interface SchemaCreatorProps {
    addNewSchema: (newSchema: SchemaInfo) => void;
}

const SchemaCreator: React.FC<SchemaCreatorProps> = ({ addNewSchema }) => {
    const [schemaInfo, setSchemaInfo] = useState<SchemaInfo>({ name: '', insights: [] })

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { value, type, checked } = e.target;

        if (type !== 'checkbox') {
            setSchemaInfo((prevData) => ({
                ...prevData,
                name: value,
            }));
            return;
        }
        if (checked) {
            setSchemaInfo((prevData) => ({
                ...prevData,
                insights: [...prevData.insights, value],
            }));
            return;
        }
        setSchemaInfo((prevData) => ({
            ...prevData,
            insights: prevData.insights.filter((insight) => insight !== value),
        }));
    };

    const handleFormSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        addNewSchema(schemaInfo);
    }

    return (
        <div className="border rounded-lg p-4 m-4 mt-20">
            <h3 className="text-lg font-bold mb-2">Schema Creator</h3>
            <form onSubmit={handleFormSubmit}>
                <label>
                    Name:
                </label>
                <input
                    type="text"
                    name="name"
                    className="bg-gray-700 rounded-md text-white px-2"
                    value={schemaInfo.name}
                    onChange={handleInputChange}
                />
                <FeatureChecklist handleInputChange={handleInputChange} />
                <button type="submit">
                    Create
                </button>
            </form>
        </div>
    )
}

export default SchemaCreator