import { AlertType } from '@/enums/enums';

export interface Bug{
    publicId: string,
    status: string,
    title: string,
    assigneeName: string,
    openedOn: string,
    description: string,
    userId: string,
    id: string
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

export interface Alert{
    showAlert: boolean,
    alertType: AlertType,
    alertMessage: string
}

export interface BugStatusUpdate{
    status: string,
    bugId: string
}

