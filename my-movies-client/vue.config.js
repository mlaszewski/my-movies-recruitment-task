module.exports = {
    devServer: {
        port: 3000,
        proxy: {
            '^/api': {
                target: "https://localhost:7053/",
                ws: true,
                changeOrigin: true
            },
            '^/MyMovies': {
                target: "https://filmy.programdemo.pl/",
                ws: true,
                changeOrigin: true
            }
        }
    }
}
