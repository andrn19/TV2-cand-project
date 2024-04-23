import { useState } from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass';
import { IP_ADDRESS } from '../../globalVars';

const ANALYSE_API_ENDPOINT = `${IP_ADDRESS}/get-metadata/`

interface AnalyseVideoButtonProps {
  addNewReceivedVideoData: (newData: VideoMetadateClass) => void;
}

const AnalyseVideoButton: React.FC<AnalyseVideoButtonProps> = ({ addNewReceivedVideoData }) => {
  const [videoURL, setVideoURL] = useState('')


  const getMetadataFromVideoID = async (videoId: string) => {
    const requestOptions = {
      method: 'GET',
      headers: {
        Accept: 'text/plain',
      },
    };

    try {
      const response = await fetch(`${ANALYSE_API_ENDPOINT}${videoId}`, requestOptions)
      if (response.ok) {
        const videoMetadata = await response.json()
        console.log(videoMetadata)
        addNewReceivedVideoData(videoMetadata)
      } else {
        console.error('Failed to fetch metadata:', response.statusText)
      }
    }
    catch (e) {
      console.log(e)
    }
  }

  const handleAnalyseClick = async (event: React.FormEvent) => {
    event.preventDefault()

    const requestOptions = {
      method: 'GET',
      headers: {
        Accept: 'text/plain',
      },
    };

    try {
      const response = await fetch(`${ANALYSE_API_ENDPOINT}${videoURL}`, requestOptions)
      if (response.ok) {
        const videoMetadata = await response.json()
        console.log(videoMetadata)
        //NEEDS TO FETCH THE METADATA EFTER USING getMetadataFromVideoID
        addNewReceivedVideoData(videoMetadata)
      } else {
        console.error('Failed to fetch metadata:', response.statusText)
      }
    }
    catch (e) {
      console.log(e)
    }
  }

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setVideoURL(event.target.value)
  };

  return (
    <div>
      <form onSubmit={handleAnalyseClick}>
        <input
          id='videoId'
          value={videoURL}
          onChange={handleInputChange}
          className="appearance-none bg-gray-700 rounded-md p-.5 text-white w-full mt-2"
        />
        <button type="submit">Analyse Video</button>
      </form>
    </div>
  )
}

export default AnalyseVideoButton