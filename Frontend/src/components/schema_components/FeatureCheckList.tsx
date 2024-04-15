import React, { useState } from 'react';

interface FeatureChecklistProps {
    handleInputChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const featureOptions = ['Face Detection', 'Topics Extraction', 'Keywords Extraction', 'Labels Identification', 'Named Entities', 'OCR', 'Transcription']

const FeatureChecklist: React.FC<FeatureChecklistProps> = ({ handleInputChange }) => {

    return (
        <div>
            <h3 className="text-lg font-bold mb-2">Insight List</h3>
            {featureOptions.map(feature => (
                <div key={feature}>
                    <input
                        name={feature}
                        value={feature}
                        type="checkbox"
                        onChange={handleInputChange}
                        className="mr-2"
                    />
                    <span>{feature}</span>
                </div>
            ))}
        </div>
    );
};

export default FeatureChecklist;