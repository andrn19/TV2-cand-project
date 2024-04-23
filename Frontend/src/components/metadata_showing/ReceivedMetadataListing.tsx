import {useState} from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass'

import ReceivedVideoPreview from './ReceivedVideoPreview'
import MetadataWindow from './MetadataWindow'

interface ShowMetadataInterface {
    show: boolean;
    file: VideoMetadateClass | undefined;
}

type ReceivedMetadataListingProps = {
    receivedVideoData: VideoMetadateClass[]
}

const ReceivedMetadataListing: React.FC<ReceivedMetadataListingProps> = ({receivedVideoData}) => {
    const [showMetadata, setShowMetadata] = useState<ShowMetadataInterface>({ show: false, file: undefined });

    const onEdit = (file: VideoMetadateClass) => {
        setShowMetadata({ show: true, file: file });
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
                    {receivedVideoData.map(file => (
                        <div key={file.videoId} >
                            <ReceivedVideoPreview file={file} onEdit={() => onEdit(file)} />
                        </div>
                    ))}
                </ul>
            </div>
            <div>
                {showMetadata.show && <MetadataWindow file={showMetadata.file} onClose={closeEditWindow} />}
            </div>
        </>
  )
}

export default ReceivedMetadataListing