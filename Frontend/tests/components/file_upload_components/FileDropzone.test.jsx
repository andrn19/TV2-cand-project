import { describe, expect, it, vi } from 'vitest';
import { render, fireEvent, screen } from '@testing-library/react';
import { act } from 'react-dom/test-utils';
import FileDropzone from '../../../src/components/file_upload_components/FileDropzone';

describe("create files", () => {
    let files;

    beforeEach(() => {
        const file1 = createFile('file1', 2000, 'video/mp4');
        const file2 = createFile('file2', 2000, 'video/avi');

        files = [
            file1,
            file2
        ];
    });

    afterEach(() => {
        vi.clearAllMocks()
    });

    describe('FileDropzone', () => {
        it('calls onFilesDrop callback with accepted video files when files are dropped', async () => {
            const event = createDataTransferWithFiles(files)
            const onFilesDropMock = vi.fn();

            render(<FileDropzone onFilesDrop={onFilesDropMock} />);

            const dropzone = screen.getByTestId('drop-zone');

            await act(() => fireEvent.drop(dropzone, event));

            expect(onFilesDropMock).toHaveBeenCalledWith(files);
        });
    });

    it('calls onFilesDrop callback with accepted video files when files are selected using input', async () => {
        const onFilesDropMock = vi.fn();

        render(<FileDropzone onFilesDrop={onFilesDropMock} />);

        const input = screen.getByTestId('file-input');
        await act(() => fireEvent.change(input, { target: { files: [] } }));

        expect(onFilesDropMock).toHaveBeenCalledOnce();
    });
});

function createFile(name, size, type) {
    const file = new File([], name, { type });
    Object.defineProperty(file, "size", {
        get() {
            return size;
        },
    });
    return file;
}

function createDataTransferWithFiles(files = []) {
    return {
        dataTransfer: {
            files,
            items: files.map((file) => ({
                kind: "file",
                size: file.size,
                type: file.type,
                getAsFile: () => file,
            })),
            types: ["Files"],
        },
    };
}