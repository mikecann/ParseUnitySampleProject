declare module Parse {

    var applicationId: string;

    module Analytics {
        function track(name: string, dimensions: any);
    }

    module Push {

        interface PushData {
            channels?: string[];
            push_time?: Date;
            expiration_time?: Date;
            expiration_interval?: Date;
            where?: Parse.Query;
            data?: any;
        }

        function send(data: PushData, options?: CallbackOptionsBase): PromiseBase;
    }

    module Cloud {

        interface HTTPResponse {
            buffer: any; // Buffer
            cookies: any;
            data: any;
            headers: any;
            status: number;
            text: string;
        }

        interface HTTPOptions {
            body?: string;
            headers?: any;
            method?: any;
            url: string;
            params?: any;            
            success?: (response: HTTPResponse) => void;
            error?: (response: HTTPResponse) => void;
        }

        class AfterSaveRequest {
            installationId: string;
            master: boolean;
            object: Object;
            user: User;
        }

        class BeforeSaveRequest {
            installationId: string;
            master: boolean;
            object: Object;
            user: User;
        }

        class BeforeSaveResponse {
            error(msg?:string);
            success(obj?:Parse.Object)
        }

        function useMasterKey(): void;
        function httpRequest(options: HTTPOptions): Promise<HTTPResponse>;
        function define(callName: string, handler: (request: ParseRequest, response: ParseResponse) => void): any;
        function run(functionName: string, args?: any, options?: CallbackOptions<any>): Promise<any>;
        function afterSave(className: string, handler: (request: AfterSaveRequest) => void);
        function afterSave(classDefinition: any, handler: (request: AfterSaveRequest) => void);
        function beforeSave(className: string, handler: (request: BeforeSaveRequest, response: BeforeSaveResponse) => void);
        function beforeSave(classDefinition: any, handler: (request: BeforeSaveRequest, response: BeforeSaveResponse) => void);
       
    }

    class ACL {
        getPublicReadAccess(): boolean;
        getPublicWriteAccess(): boolean;
        getReadAccess(userId: User): boolean;
        getRoleReadAccess(role: string): boolean;
        //getRoleReadAccess(role: ParseRole): boolean;
        getRoleWriteAccess(role: string): boolean;
        //getRoleWriteAccess(role: ParseRole): boolean;
        getWriteAccess(userId: User): boolean;
        setPublicReadAccess(allowed: boolean): void;
        setPublicWriteAccess(allowed: boolean): void;
        setReadAccess(userId: User, allowed: boolean): void;
        setReadAccess(userObjectId: string, allowed: boolean): void;
        setWriteAccess(userId: User, allowed: boolean): void;
        setWriteAccess(userId: string, allowed: boolean): void;
        setRoleReadAccess(allowed: boolean): void;
        toJSON(): any;
    }

    var Installation: ParseInstallation;
    //var Object: Object;

    function initialize(appKey: string, jsKey: string): any;

    interface CallbackOptionsBase {
        success: (results: any) => void;
        error: (error?: any) => void;
    }

    interface CallbackOptions<T> {
        success: (results: T) => void;
        error: (error?: any) => void;
    }

    class ParseInstallation extends Object {
    }

    class PromiseBase {
        //then(resultsCallback: (...PromiseBase: any[]) => PromiseBase, errorCallback?: (error: any) => void): PromiseBase;
        then<T>(resultsCallback: () => Promise<T>, errorCallback?: (error: any) => void): Promise<T>;
        then(resultsCallback: () => PromiseBase, errorCallback?: (error: any) => void): PromiseBase;
        then(resultsCallback: () => void, errorCallback?: (error: any) => void): PromiseBase;
        //then(resultsCallback: (...PromiseBase : any[]) => void, errorCallback?: (error:any) => void): PromiseBase;
        //then(resultsCallback: (...PromiseBase : any[]) => void, errorCallback?: (_:any, error:any) => void): PromiseBase;
        resolve(result?: any): any;
        reject(error?: any): any;
        always(callback: () => void);
        fail(errorCallback: (err: any) => any): PromiseBase;
    }

    class Promise<T> extends PromiseBase {
        static as(): PromiseBase;
        static as<U>(value: U): Promise<U>;
        static when(...args: PromiseBase[]): ResfulPromise;
        static when<T>(promises: Promise<T>[]): Promise<T>;
        static error(msg: string): PromiseBase;
        static error(data: any): PromiseBase;
        then<U>(resultsCallback: (result: T) => Promise<U>, errorCallback?: (error: any) => void): Promise<U>;
        then(resultsCallback: (result: T) => void, errorCallback?: (error: any) => void): PromiseBase;
        fail(errorCallback: (err: any) => any): Promise<T>;
    }

    class ResfulPromise extends PromiseBase {
        then<T>(resultsCallback: (...results:any[]) => Promise<T>, errorCallback?: (error: any) => void): Promise<T>;
        then(resultsCallback: (...results: any[]) => PromiseBase, errorCallback?: (error: any) => void): PromiseBase;
        then(resultsCallback: (...results: any[]) => void, errorCallback?: (error: any) => void): PromiseBase;
    }

    class Error {
        code: number;
        message: string;
    }

    class Query {
        constructor(subclass: ObjectSubclassDefinition);
        constructor(parseObjectClassName: string);
        equalTo(key: string, value: any): Query;
        notEqualTo(key: string, value: any): Query;
        greaterThanOrEqualTo(key: string, value: any): Query;
        lessThan(key: string, value: any): Query;
        matchesKeyInQuery(key: string, queryKey:string, query:Query): Query;
        include(key: string): Query;
        contains(key: string, substring:string): Query;
        descending(key: string): Query;
        ascending(key: string): Query;        
        limit(count: number): Query;
        notContainedIn(key: string, values: any[]): Query;
        containedIn(key: string, values: any[]): Query;
        get(objectId: string, options?: CallbackOptions<Object>): Promise<Object>;
        find(options?: CallbackOptionsBase): Promise<Object[]>;
        first(options?: CallbackOptionsBase): Promise<Object>;
        static or(...queries: Query[]): Query;
        matchesQuery(key: string, query: Query): Query;
    }

    interface ObjectSubclassDefinition {
        new (attributes?: any, options?: any): Object;
    }

    class Object {
        constructor(className: string);
        id: string;
        static extend(className: string): ObjectSubclassDefinition;
        save(options?: CallbackOptionsBase): Promise<Object>;
        static destroyAll(list: Object[], options?: CallbackOptionsBase): PromiseBase;
        static saveAll(list: Object[], options?: CallbackOptionsBase): PromiseBase;
        add(attr: string, item: any);
        addUnique(attr: string, item: any);
        change(options?: any);
        changedAttributes(diff?: any): void;
        clear(options?: any);
        dirty(attr?: string): boolean;
        destroy(options?: CallbackOptionsBase): Promise<Object>;
        save(): Promise<Object>;
        save(attrs: any, options?: CallbackOptionsBase): Promise<Object>;
        save(key: string, value: any, options?: CallbackOptionsBase): Promise<Object>;
        setACL(acl: ACL, options?: any): boolean;
        set(key: string, value: any): any;
        get(key: string): any;
        fetch(): Promise<Object>;
        createdAt: Date;
        updatedAt: Date;
        attributes: any;
        existed(): boolean;
    }

    class File {
        constructor(name: string, data : { base64:string }, contentType?: string);
        constructor(name: string, data: Uint8Array, contentType?: string);
        url(): string;
        name(): string;
        save(options?: CallbackOptionsBase): PromiseBase;
    }

    interface ParseRequest {
        params: any;
        user: User;
    }

    interface ParseResponse {
        success(data: any): any;
        error(error: any): any;
    }

    class User extends Object {
        constructor();
        static become(sessionToken: string, options?: CallbackOptions<User>): Promise<User>;
        static current(): User;
        static logOut();
        static logIn(username: string, password: string, options?: CallbackOptions<User>): Promise<User>;
        static signUp(username: string, password: string, attrs?: any, options?: CallbackOptions<User>): Promise<User>;
        static requestPasswordReset(email: string, options?: CallbackOptionsBase) : PromiseBase;
        signUp(attrs?: any, options?: CallbackOptions<User>): Promise<User>;
        logIn(options?: CallbackOptions<User>): Promise<User>;
        fetch(): Promise<User>;
        getSessionToken(): string;
        getUsername(): string;
        getEmail(): string;
        setUsername(username: string);
        setEmail(email: string);
        setPassword(password: string);
    }

}

//declare module "parse"
//{  
//    export = Parse;
//}
