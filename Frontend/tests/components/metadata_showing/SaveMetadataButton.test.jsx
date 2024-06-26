import { describe, expect, it, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import { IP_ADDRESS } from '../../../src/globalVars';
import SaveMetadataButton from '../../../src/components/metadata_showing/SaveMetadataButton'

const SAVE_METADATA_ENDPOINT = `${IP_ADDRESS}/add-metadata`;

// Mock the fetch function
global.fetch = vi.fn();

describe('SaveMetadataButton', () => {

    it('renders the button', () => {
        render(<SaveMetadataButton metadata={{ key: 'value' }} />);
        expect(screen.getByText('SaveMetadataButton')).toBeInTheDocument();
    });

    it('calls fetch with the correct parameters when clicked', async () => {
        const metadata = { key: 'value' };
        global.fetch.mockResolvedValueOnce({
            ok: true,
        });

        render(<SaveMetadataButton metadata={metadata} />);

        const button = screen.getByText('SaveMetadataButton');
        fireEvent.click(button);

        expect(global.fetch).toHaveBeenCalledTimes(1);
        expect(global.fetch).toHaveBeenCalledWith(SAVE_METADATA_ENDPOINT, {
            method: 'POST',
            headers: {
                Accept: 'text/plain',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(metadata),
        });
    });
});
