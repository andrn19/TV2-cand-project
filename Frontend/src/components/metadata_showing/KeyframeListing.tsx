import { useEffect, useState } from "react";
import { Shot, Keyframe } from "../../classes/videoMetadataClass"

interface KeyframeListingProps {
    keyframes: Shot[] | undefined;
}

const KeyframeListing: React.FC<KeyframeListingProps> = ({ keyframes }) => {
    const [selectedKeyframe, setSelectedKeyframe] = useState<Keyframe>();

    useEffect(() => {
        console.log(selectedKeyframe)
    },[selectedKeyframe])

    const handleKeyframeClick = (frame: Keyframe) => {
        setSelectedKeyframe(frame);
    };

    return (
        <div>
            {keyframes?.map(shot => (
                <div key={shot.id} className="border border-white-300 rounded p-4">
                    <div className="grid grid-cols-3 sm:grid-cols-4 md:grid-cols-6 gap-3">
                        {shot.keyFrames.map(keyframe => (
                            <div key={keyframe.instances[0].thumbnailId}
                                className={`dark rounded ${selectedKeyframe === keyframe ? 'border border-bright border-4' : ''}`}
                                onClick={() => handleKeyframeClick(keyframe)}
                            >
                                <img src={keyframe.instances[0].thumbnailId} />
                                <p>{keyframe.instances[0].thumbnailId}</p>
                            </div>
                        ))}
                    </div>
                </div>
            ))};
        </div>
    );
};

export default KeyframeListing