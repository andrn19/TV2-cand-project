import {useState} from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass'

import ReceivedVideoPreview from './ReceivedVideoPreview'
import MetadataWindow from './MetadataWindow'

interface ShowMetadataInterface {
    show: boolean;
    metadateObject: VideoMetadateClass | undefined;
}

type ReceivedMetadataListingProps = {
    receivedVideoData: VideoMetadateClass[]
}

const ReceivedMetadataListing: React.FC<ReceivedMetadataListingProps> = ({receivedVideoData}) => {
    const [showMetadata, setShowMetadata] = useState<ShowMetadataInterface>({ show: false, metadateObject: undefined });

    const onEdit = (metadateObject: VideoMetadateClass) => {
        setShowMetadata({ show: true, metadateObject: metadateObject });
    }

    const closeEditWindow = () => {
        setShowMetadata(prevState => ({
            ...prevState,
            show: false
        }))
    }


  return (
    <>
            <div className="rounded-lg bg-gray-700 flex flex-col">
                <h2 className="font-bold">Received Metadata Analysis:</h2>
                <ul className='mt-6 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-10'>
                    {receivedVideoData.map(metadateObject => (
                        <div key={metadateObject.videoId} >
                            <ReceivedVideoPreview metadateObject={metadateObject} onEdit={() => onEdit(metadateObject)} />
                        </div>
                    ))}
                </ul>
            </div>
            <div>
                {showMetadata.show && <MetadataWindow metadateObject={showMetadata.metadateObject} onClose={closeEditWindow} />}
            </div>
        </>
  )
}

export default ReceivedMetadataListing