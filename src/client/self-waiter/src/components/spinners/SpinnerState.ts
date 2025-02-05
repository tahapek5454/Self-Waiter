import { reactive } from 'vue'

export const spinnerStore = reactive({
    isLoading: false,
    showLoading(){
        this.isLoading = true
    },
    hideLoading(){
        this.isLoading = false
    }   
})