
const ANALYSE_API_ENDPOINT = "http://10.244.1.74:8080/gateway/get-metadata/"

const AnalyseVideoButton = () => {


  const handleAnalyseClick = async () => {
    const videoId = ''

    const requestOptions = {
      method: 'PUT',
      headers: {
        Accept: 'text/plain',
      },
    };

    try {
      const response = await fetch(`${ANALYSE_API_ENDPOINT}${videoId}`, requestOptions);
      console.log(response)
    }
    catch (e) {
      console.log(e)
    }
  }

  return (
    <div>
      <form>
        <input
          id='videoId'
          onSubmit={handleAnalyseClick}
          style={{ color: 'black', width: '100%' }}
          className='light pl-1 pr-1 block overflow-hidden resize-none'
        />
        <button type="submit" onClick={handleAnalyseClick}>Analyse Video</button>
      </form>
    </div>
  )
}

export default AnalyseVideoButton