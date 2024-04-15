import { useState, useEffect } from 'react'
import { signal } from '@preact/signals-react';
import { useSignals } from '@preact/signals-react/runtime';
import { DataInterface } from '../../interfaces';

import DatabaseConnector from './DatabaseConnector'
import DatabaseEndpointCreator from './DatabaseEndpointCreator'

const ENDPOINT_LIST_API_URL = 'http://localhost:52001/gateway/list-endpoints';

const DatabaseConnectorTab = () => {
  const [databaseConnectors, setDatabaseConnectors] = useState<DataInterface[]>([]);


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


  return (
    <div className="flex justify-around">
      <DatabaseConnector databaseConnectors={databaseConnectors} />
      <DatabaseEndpointCreator fetchEndpointList={fetchEndpointList}/>
    </div>
  )
}

export default DatabaseConnectorTab