import React, { useState } from 'react';

interface FeatureChecklistProps {
    handleInputChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const featureOptions = {
    faces: 'Face Detection',
    topics: 'Topics Extraction',
    keywords: 'Keywords Extraction',
    labels: 'Labels Identification',
    namedLocations: 'Mentiend Location',
    namedPeople: 'Mentiend People',
    transcript: 'Transcription',
    emotions: 'Sentiments'
};

const FeatureChecklist: React.FC<FeatureChecklistProps> = ({ handleInputChange }) => {

    return (
        <>
            <label className="text-lg font-bold mb-2">Insight List</label>
            <div className="flex flex-col items-center">
                {Object.values(featureOptions).map(feature => (
                    <div key={feature} className="flex flex-row">
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
        </>
    );
};

export default FeatureChecklist;