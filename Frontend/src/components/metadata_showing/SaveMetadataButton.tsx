import React from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass'
import { IP_ADDRESS } from '../../globalVars'

type SaveMetadataButtonProps = {
    metadata: VideoMetadateClass
}

const SAVE_METADATA_ENDPOINT = `${IP_ADDRESS}/AddMetadata`

const SaveMetadataButton: React.FC<SaveMetadataButtonProps> = ({ metadata }) => {

    const handleSaveClick = async () => {

        const requestOptions = {
            method: 'POST',
            headers: {
                Accept: 'text/plain',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(metadata),
        };

        try {
            const response = await fetch(SAVE_METADATA_ENDPOINT, requestOptions);
            if (response.ok) {

            }
            else {
                console.error('Failed with status', response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    return (
        <button onClick={handleSaveClick}>SaveMetadataButton</button>
    )
}

export default SaveMetadataButton