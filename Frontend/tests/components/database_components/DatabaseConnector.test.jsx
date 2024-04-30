import { describe, expect, it, vi } from 'vitest';
import { render, fireEvent, screen } from '@testing-library/react';
import DatabaseConnector from '../../../src/components/database_connector_components/DatabaseConnector'

describe("Mocking connectors", () => {
    let databaseConnectors;

    beforeEach(() => {
        databaseConnectors = [
            { key: '1', value: 'Database 1' },
            { key: '2', value: 'Database 2' },
        ];
    });

    afterEach(() => {
        vi.clearAllMocks()
    });

    describe('DatabaseConnector component', () => {
        it('selecting an option calls handleSelectChange', () => {
            render(
                <DatabaseConnector
                    databaseConnectors={databaseConnectors}
                />
            );

            const select = screen.getByTestId('database-selector');

            const mockEvent = { target: { value: JSON.stringify(databaseConnectors[0]) } };
            fireEvent.change(select, mockEvent);

            const selectedOption = databaseConnectors[0];
            const selectedConnectorElement = screen.getByText(selectedOption.value);
            expect(selectedConnectorElement).toBeInTheDocument();
        });

        // TO BE IMPLEMENTED
        // it('handleConnectClick is called when Connect button is clicked', () => {

        //     const handleConnectClickMock = vi.fn();

        //     render(
        //         <DatabaseConnector
        //             databaseConnectors={databaseConnectors}
        //             removeConnector={() => { }}
        //         />
        //     );

        //     const connectButton = screen.getByTestId('database-connect-btn');
        //     fireEvent.click(connectButton);

        //     expect(handleConnectClickMock).toHaveBeenCalledOnce();
        // });

        it('handleRemoveClick is called when Remove button is clicked', () => {

            const handleRemoveClickMock = vi.fn();

            render(
                <DatabaseConnector
                    databaseConnectors={databaseConnectors}
                    removeConnector={handleRemoveClickMock}
                />
            );

            const select = screen.getByTestId('database-selector');

            const mockEvent = { target: { value: JSON.stringify(databaseConnectors[0]) } };
            fireEvent.change(select, mockEvent);

            const removeButton = screen.getByTestId('database-remove-btn');
            fireEvent.click(removeButton);

            expect(handleRemoveClickMock).toHaveBeenCalledOnce();
        });

        it('handleRemoveClick is not called when Remove button is clicked and no option is selected', () => {
            const handleRemoveClickMock = vi.fn();
        
            render(
              <DatabaseConnector
                databaseConnectors={databaseConnectors}
                removeConnector={handleRemoveClickMock}
              />
            );
        
            const removeButton = screen.getByTestId('database-remove-btn');
            fireEvent.click(removeButton);
        
            expect(handleRemoveClickMock).not.toHaveBeenCalled();
          });
    });
});