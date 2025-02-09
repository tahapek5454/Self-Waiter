import { createRouter, createWebHashHistory } from 'vue-router';

const routes = [
    {
        path: '',
        name: 'CustomerLayout',
        component: () => import('../views/customers/layout.vue'),
        children: [
            {
                path: '',
                name: 'CustomerHome',
                component: () => import('../views/customers/home/index.vue'),
            }
        ]
    },
    {
        path: '/management',
        name: 'ManagementLayout',
        component: () => import('../views/managements/layout.vue'),
        children: [
            {
                path: '',
                name: 'ManagementHome',
                component: () => import('../views/managements/home/index.vue'),
            }
        ],
    }
];
// import.meta.env.BASE_URL
const router = createRouter({
    history: createWebHashHistory(),
    routes: routes,
});

export default router;