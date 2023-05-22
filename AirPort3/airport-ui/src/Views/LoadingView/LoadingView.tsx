import "./LoadingView.css";

export function LoadingView() {
    return (
        <div className="LoadingView">
            <h1>Trying To Connect ... </h1>
            <h2>
                Please Check if the Server is Connected and refresh this page
            </h2>
            <button
                onClick={() => {
                    location.reload();
                }}
            >
                Refresh
            </button>
        </div>
    );
}
