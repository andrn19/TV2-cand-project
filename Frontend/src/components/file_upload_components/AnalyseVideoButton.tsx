import { useState } from 'react'
import { VideoMetadateClass } from '../../classes/videoMetadataClass';
import { IP_ADDRESS } from '../../globalVars';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const ANALYSE_API_ENDPOINT = `${IP_ADDRESS}/get-metadata/`

interface AnalyseVideoButtonProps {
  addNewReceivedVideoData: (newData: VideoMetadateClass) => void;
}

const AnalyseVideoButton: React.FC<AnalyseVideoButtonProps> = ({ addNewReceivedVideoData }) => {
  const [videoURL, setVideoURL] = useState('')

  const notify = () => toast('Video Uploaded', { autoClose: 3000, toastId: 1, theme: 'dark' })

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
        const responseJSON = await response.json()
        const videoMetadata = new VideoMetadateClass(responseJSON)
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

    notify()

    const requestOptions = {
      method: 'GET',
      headers: {
        Accept: 'text/plain',
      },
    };

    try {
      const response = await fetch(`${ANALYSE_API_ENDPOINT}${videoURL}`, requestOptions)
      if (response.ok) {
        const videoID = await response.json()
        console.log(videoID)
        getMetadataFromVideoID(videoID)
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
          placeholder='Video URL'
          onChange={handleInputChange}
          className="appearance-none bg-gray-700 rounded-md p-.5 text-white w-full mt-2"
        />
        <button type="submit">Analyse Video</button>
      </form>
    </div>
  )
}

export default AnalyseVideoButton