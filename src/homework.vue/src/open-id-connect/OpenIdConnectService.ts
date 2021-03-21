import { UserManager, User } from 'oidc-client';
import { openIdConnectSettings } from '@/open-id-connect/Config';

export class OpenIdConnectService {
    public static getInstance(): OpenIdConnectService {
        if (!this.instance) {
            this.instance = new OpenIdConnectService();
        }
        return this.instance;
    }

    private static instance: OpenIdConnectService;

    private userManager = new UserManager(openIdConnectSettings);

    private currentUser!: User | null;
    

    //userLoaded$=new ReplaySubject<boolean>(1);

    private constructor() {
        // 清理过期的东西
        this.userManager.clearStaleState();

        this.userManager.getUser().then((user) => {
           
            if (user) {//如果登陆了
                this.currentUser = user;
            } else {//如果没登录
                this.currentUser = null;
            }
        }).catch((err) => {
            this.currentUser = null;
        });

        // 在建立（或重新建立）用户会话时引发
        this.userManager.events.addUserLoaded((user) => {
            console.log('addUserLoaded', user);
            this.currentUser = user;
        });

        // 终止用户会话时引发
        this.userManager.events.addUserUnloaded(()=>{
            console.log('addUserUnloaded', this.user);
            this.currentUser = null;
        });
    }

    // 当前用户是否登录
    get userAvailavle(): boolean {
        return !!this.currentUser;
    }
    async login () {
        await this.triggerSignIn();
        return this.currentUser;
      }
    // 获取当前用户信息
    get user(): User {
        return this.currentUser as User;
    }

    // 触发登录
    public async triggerSignIn() {
        await this.userManager.signinRedirect();
        console.log('triggerSignIn');
    }

    // 登录回调
    public async handleCallback() {
        const user: User = await this.userManager.signinRedirectCallback();
        console.log('handleCallback');
        this.currentUser = user;
    }

    // 自动刷新回调
    public async handleSilentCallback() {
        this.userManager.signinSilentCallback()
        .then(res=>{
            if(typeof(res)=="undefined")
            this.currentUser = null;
            else{
                this.currentUser=res;
            }
        });
        console.log('handleSilentCallback');
    }

    // 触发登出
    public async triggerSignOut() {
        console.log('triggerSignOut');
        await this.userManager.signoutRedirect();
    }
}