import { describe, expect, it, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import MetadataShowing from '../../../src/components/metadata_showing/MetadataShowing'

describe("Mocking file and metadata object", () => {
    let file;
    let metadataObject;

    beforeEach(() => {
        file = { videoName: 'Sample Video' };
        metadataObject = {
            videoId: 'id',
            videoName: 'name',
            publishedUrl: 'url',
            metadata: {
                topics: [],
                labels: [],
                keywords: [],
                namedLocations: [],
                namedPeople: [],
                emotions: [],
                faces: [],
                transcript: [{ id: 1, text: 'Initial transcript', instances: [{ adjustedStart: 0, adjustedEnd: 10 }] }],
                shots: [],
            }
        };
    });

    afterEach(() => {
        vi.clearAllMocks()
    });
    describe('MetadataShowing component', () => {
        it('the correct amount of metadata divs are rendered', () => {
            render(<MetadataShowing metadateObject={metadataObject} />);

            expect(screen.getAllByTestId('metadata-div').length).toBe(8)
        });

        it('changes can be made in the metadata textareas', () => {

            render(<MetadataShowing metadateObject={metadataObject} />);

            const transcriptTextarea = screen.getByTestId('transcript-textarea');

            fireEvent.change(transcriptTextarea, { target: { value: 'Edited transcript' } });

            expect(transcriptTextarea).toHaveValue('Edited transcript');
        });
    });
});