import React, { useState, useEffect } from 'react';
import { FileTree } from '../models/FileTree';
import { ProjectMetrics } from '../models/ProjectMetrics';

interface ProjectExplorerProps {
    projectPath: string;
}

export const ProjectExplorer: React.FC<ProjectExplorerProps> = ({ projectPath }) => {
    const [fileTree, setFileTree] = useState<FileTree | null>(null);
    const [metrics, setMetrics] = useState<ProjectMetrics | null>(null);

    useEffect(() => {
        // Implementation coming soon
    }, [projectPath]);

    return (
        <div className="project-explorer">
            {/* Implementation coming soon */}
        </div>
    );
};
