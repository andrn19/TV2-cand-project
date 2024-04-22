import { useState, useEffect } from 'react'
import { DataInterface, EndpointFormData } from '../../interfaces';

import DatabaseConnector from './DatabaseConnector'
import DatabaseEndpointCreator from './DatabaseEndpointCreator'

const ENDPOINT_LIST_API_URL = 'http://10.244.1.74:8080/gateway/list-endpoints';

const DatabaseConnectorTab = () => {
  const [databaseConnectors, setDatabaseConnectors] = useState<DataInterface[]>([{key:'hhhh', value: 'wwerwe'}]);


  const fetchEndpointList = () => {
    fetch(ENDPOINT_LIST_API_URL, {
      method: 'GET',
      headers: {
        'accept': 'text/plain'
      }
    })
      .then(response => response.json())
      .then(data => setDatabaseConnectors(data))
      .catch(error => console.error(error));
  }

  useEffect(() => {
    fetchEndpointList()
  }, [])

  const isDupInConnectors = (newConnector: EndpointFormData) => {
    const existingConnector = databaseConnectors.find(connector => connector.key === newConnector.name);

    if (existingConnector) {
      return true
    }
    return false
  }

  const removeConnector = (connectorKey: string) => {
    const updatedConnectors = databaseConnectors.filter(connector => connector.key !== connectorKey);

    setDatabaseConnectors(updatedConnectors);
  };


  return (
    <div className="flex justify-around">
      <DatabaseConnector databaseConnectors={databaseConnectors} removeConnector={removeConnector} />
      <DatabaseEndpointCreator fetchEndpointList={fetchEndpointList} isDupInConnectors={isDupInConnectors} />
    </div>
  )
}

export default DatabaseConnectorTab