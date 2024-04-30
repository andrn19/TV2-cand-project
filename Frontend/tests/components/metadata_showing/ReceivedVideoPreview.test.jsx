import { describe, expect, it, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import ReceivedVideoPreview from '../../../src/components/metadata_showing/ReceivedVideoPreview'

describe('ReceivedVideoPreview component', () => {
    it('onEdit function is called when edit button is clicked', () => {
        const file = {
            videoId: '1322',
            videoName: 'Sample Video',
            publishedUrl: 'sample-url.mp4',
            metadata: ''
        };

        const onEditMock = vi.fn();

        render(
            <ReceivedVideoPreview
                file={file}
                onEdit={onEditMock}
            />
        );

        fireEvent.mouseEnter(screen.getByTestId('video-preview'));

        const editButton = screen.getByTestId('edit-btn');
        fireEvent.click(editButton);

        expect(onEditMock).toHaveBeenCalledOnce();
    });
});