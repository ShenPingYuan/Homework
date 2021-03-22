const apihost = 'https://localhost:5001';

// 这是统一设置接口路径的位置
export const endPoint = {
    // tslint:disable-next-line: jsdoc-format
    QueryTodos: `${apihost}/api/todo`,
    // 添加 todo
    AddTodo: `${apihost}/api/todo`,
    //获取菜单
    GetMenus: `${apihost}/api/menus`,
    //初始化老师
    InitTeacher:`${apihost}/api/teacher/initteacher`,
    //初始化学生
    InitStudent:`${apihost}/api/initstudent`,
};