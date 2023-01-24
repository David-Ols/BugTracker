export interface Bug{
    publicId: string,
    status: string,
    title: string,
    assigneeName: string,
    openedOn: string,
    description: string
}

export interface User{
    id: string | null,
    name: string
}