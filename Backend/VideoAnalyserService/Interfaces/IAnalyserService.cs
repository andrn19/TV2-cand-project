namespace TV2.Backend.Services.VideoAnalyser.Interfaces;
using Client.Model;

public interface IAnalyserService
{
    /// <summary>
    /// Uploads a video and starts the video index. Calls the uploadVideo API
    /// </summary>
    /// <param name="videoUrl"> Link To Publicly Accessed Video URL</param>
    /// <param name="videoName"> The Asset name to be used </param>
    /// <param name="account"> An account object </param>
    /// <param name="accountAccessToken"> An ARM access token </param>
    /// <param name="exludedAIs"> The ExcludeAI list to run </param>
    /// <param name="waitForIndex"> should this method wait for index operation to complete </param>
    /// <returns> Video Id of the video being indexed, otherwise throws excpetion</returns>
    Task<string> UploadUrlAsync(string videoUrl, string videoName, Account account, string accountAccessToken, string exludedAIs = null, bool waitForIndex = false);
    
    
    
    /// <summary>
    /// Calls getVideoIndex API in 10 second intervals until the indexing state is 'processed'
    /// </summary>
    /// <param name="videoId"> The video id </param>
    /// <param name="account"> An account object </param>
    /// <param name="accountAccessToken"> An ARM access token </param>
    /// <param name="schema"> A schema for the metadata </param>
    /// <returns> Returns video index when the index is complete, otherwise throws exception </returns>
    Task<Index> WaitForIndexAsync(string videoId, Account account, string accountAccessToken, string schema);
    Task<bool> WaitForProgressAsync(List<string> videoIds, Account account, string accountAccessToken);
}