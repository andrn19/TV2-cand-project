import { useState } from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass';

const ANALYSE_API_ENDPOINT = "http://localhost:8080/gateway/get-metadata/"

interface AnalyseVideoButtonProps {
  addNewReceivedVideoData: (newData: VideoMetadateClass) => void;
}

const AnalyseVideoButton: React.FC<AnalyseVideoButtonProps> = ({ addNewReceivedVideoData }) => {
  const [videoId, setVideoId] = useState('')

  const handleAnalyseClick = async (event: React.FormEvent) => {
    event.preventDefault()

    const requestOptions = {
      method: 'PUT',
      headers: {
        Accept: 'text/plain',
      },
    };

    try {
      const response = await fetch(`${ANALYSE_API_ENDPOINT}${videoId}`, requestOptions)
      if (response.ok) {
        const videoMetadata = await response.json()
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
    setVideoId(event.target.value)
  };

  return (
    <div>
      <form onSubmit={handleAnalyseClick}>
        <input
          id='videoId'
          value={videoId}
          onChange={handleInputChange}
          className="appearance-none bg-gray-700 rounded-md p-.5 text-white w-full"
        />
        <button type="submit">Analyse Video</button>
      </form>
    </div>
  )
}

export default AnalyseVideoButton