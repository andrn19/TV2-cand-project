import { describe, expect, it, vi } from 'vitest';
import { render, fireEvent, screen } from '@testing-library/react';
import FileList from '../../../src/components/file_upload_components/FileList'

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

    describe('FileList', () => {
        it('hovers over a file preview, and click the remove button to remove the file', () => {
            const updateFilesMock = vi.fn();

            render(<FileList files={files} updateFiles={updateFilesMock} />);

            const preview = screen.getAllByTestId("uploadPreview")

            fireEvent.mouseOver(preview[0])

            const removeBtn = screen.getByTestId("removePreviewBtn")

            fireEvent.click(removeBtn)

            expect(updateFilesMock).toHaveBeenCalledOnce();
        });
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