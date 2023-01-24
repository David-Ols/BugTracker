export interface Bug{
    publicId: string,
    status: string,
    title: string,
    assigneeName: string,
    openedOn: string,
    description: string,
    userId: string
}

export interface User{
    id: string | null,
    name: string
}

export interface CreateBug{
    title: string,
    description: string,
    userId: string
}

