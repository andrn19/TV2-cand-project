import React, { useState } from 'react';

interface CheckedItems {
    [itemName: string]: boolean;
}

const featureOptions = ['Face Detection', 'Topics Extraction', 'Keywords Extraction', 'Labels Identification', 'Named Entities', 'OCR', 'Transcription']

const FeatureChecklist: React.FC = () => {
    const [checkedItems, setCheckedItems] = useState<CheckedItems>({});

    const handleCheckboxChange = (itemName: string) => {
        setCheckedItems((prevCheckedItems) => ({
            ...prevCheckedItems,
            [itemName]: !prevCheckedItems[itemName],
        }));
    };

    const handleSubmit = () => {
        // Perform actions with the checked items
        console.log('Checked Items:', checkedItems);
    };

    return (
        <div className="border rounded-lg p-4 m-4">
            <h2 className="text-lg font-bold mb-2">Checklist Component</h2>
            <form className="grid grid-cols-2 gap-4">

                {featureOptions.map(feature => (
                    <div key={feature}>
                        <input
                            type="checkbox"
                            checked={checkedItems[feature] || false}
                            onChange={() => handleCheckboxChange(feature)}
                            className="mr-2"
                        />
                        <span>{feature}</span>
                    </div>
                ))}
            </form>
            <div className="mt-4">
                <button
                    type="button"
                    onClick={handleSubmit}
                    className="bg-blue-500 text-white px-4 py-2 rounded"
                >
                    Submit
                </button>
            </div>
        </div>
    );
};

export default FeatureChecklist;