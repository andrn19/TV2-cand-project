export interface SchemaInfo {
    name: string,
    insights: string[]
}

export interface Schemas {
    schemas: SchemaInfo[]
}

export interface DatabaseEndpoint {
    name: string,
    url: string,
    port: number
}

export interface DatabaseEndpoints {
    databaseEndpoints: DatabaseEndpoint[]
}

export interface DataInterface {
    key: string,
    value: string
}

export interface EndpointFormData {
    name: string,
    url: string,
    port: number
}