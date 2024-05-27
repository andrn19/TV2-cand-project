import { describe, expect, it, vi } from 'vitest';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';

import AnalyseVideoButton from '../../../src/components/file_upload_components/AnalyseVideoButton'

global.fetch = vi.fn();

const ANALYSE_API_ENDPOINT = `http://gateway:8080/gateway/upload-footage`;
const GET_METADATA_API_ENDPOINT = `http://gateway:8080/gateway/get-metadata`;

class MockVideoMetadataClass {
    constructor(metadata) {
        this.videoId = metadata['id'];
        this.videoName = metadata['name'];
        this.publishedUrl = metadata['publishedUrl'];
        this.metadata = metadata['insights'];
    }
}

describe('AnalyseVideoButton', () => {
    afterEach(() => {
        vi.clearAllMocks();
    });

    it('calls fetch with the correct parameters', async () => {
        const videoId = '12345';
        const videoMetadata = { id: videoId, name: 'Test Video', publishedUrl: 'http://example.com/video.mp4', insights: [] };

        global.fetch
            .mockResolvedValueOnce({
                ok: true,
                json: async () => videoId,
            })
            .mockResolvedValueOnce({
                ok: true,
                json: async () => videoMetadata,
            });

        const addNewReceivedVideoData = vi.fn();

        render(<AnalyseVideoButton addNewReceivedVideoData={addNewReceivedVideoData} />);

        fireEvent.change(screen.getByPlaceholderText('Video URL'), { target: { value: 'http://example.com/video.mp4' } });
        fireEvent.change(screen.getByPlaceholderText('Video Name'), { target: { value: 'Example Video' } });

        fireEvent.submit(screen.getByText('Analyse Video'));

        await waitFor(() => {
            //console.log(global.fetch.mock.calls);
            expect(global.fetch).toHaveBeenCalledTimes(2);
            expect(global.fetch).toHaveBeenCalledWith(`${ANALYSE_API_ENDPOINT}?footageUrl=http://example.com/video.mp4&footageName=Example Video`, {
                method: 'POST',
                headers: { Accept: 'text/plain' },
            });
            expect(global.fetch).toHaveBeenCalledWith(`${GET_METADATA_API_ENDPOINT}${videoId}`, {
                method: 'POST',
                headers: { Accept: 'text/plain' },
            });

            const metadataObject = new MockVideoMetadataClass(videoMetadata)

            expect(addNewReceivedVideoData).toHaveBeenCalledWith(expect.objectContaining(metadataObject));
        });
    });
});