export class UserData{
    UserDataId?: number;
    Name: string | undefined;
    Age?: number | undefined;
    Gender: string | undefined;
    Email: string | undefined;
    Address?: string | undefined;
    MobileNumber?: string | undefined;
    ProfilePictureBase64Data?: string | undefined;
    FileName?: string | undefined;

    constructor() {
        this.UserDataId = 0;
    }
}